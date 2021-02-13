import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from 'src/app/Services/account.service';
import { MedicService } from 'src/app/Services/MedicService/medic.service';
@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit {
  formSearch: FormGroup;
  users: User[];
  constructor(public accountService: AccountService, private medicService: MedicService) { }

  ngOnInit(): void {
    this.formSearch = new FormGroup({
      "search": new FormControl(null)
    });
    this.medicService.getDoctors().subscribe(users => {
      this.users = users;
    });
  }

  submit(){
    const name =this.formSearch.get("search").value;
    this.medicService.searchDoctor(name).subscribe(users => {
      this.users = users
    });
  }
}
