import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-forgot-password',
  templateUrl: './forgot-password.component.html',
  styleUrls: ['./forgot-password.component.css']
})
export class ForgotPasswordComponent implements OnInit {
  resetForm: FormGroup;
  constructor(public accountService: AccountService) { }
  error: string = null;
  ngOnInit(): void {
    this.resetForm = new FormGroup({
      "email": new FormControl(null, [Validators.required, Validators.email])
    });
  }

  onSubmit(){
    if(this.resetForm.invalid){
      this.error = "Please enter a valid email."
    }
    else{
      console.log(this.resetForm.value.email);

    }
  }

}
