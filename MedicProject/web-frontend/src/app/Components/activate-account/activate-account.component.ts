import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-activate-account',
  templateUrl: './activate-account.component.html',
  styleUrls: ['./activate-account.component.css']
})
export class ActivateAccountComponent implements OnInit {
  activateAccountForm: FormGroup;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.activateAccountForm = new FormGroup({
      "code":new FormControl(null, Validators.required)
    })
  }

  error(){

  }

  onSubmit(){
    const token = this.activateAccountForm.get("code").value;
    this.userService.activateAccount(token);
  }
}
