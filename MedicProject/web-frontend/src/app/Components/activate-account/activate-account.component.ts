import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from 'src/app/Services/account.service';

@Component({
  selector: 'app-activate-account',
  templateUrl: './activate-account.component.html',
  styleUrls: ['./activate-account.component.css']
})
export class ActivateAccountComponent implements OnInit {
  activateAccountForm: FormGroup;
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.activateAccountForm = new FormGroup({
      "code":new FormControl(null, Validators.required)
    })
  }

  error(){

  }

  onSubmit(){
    const token = this.activateAccountForm.get("code").value;
    this.accountService.activateAccount(token);
  }
}
