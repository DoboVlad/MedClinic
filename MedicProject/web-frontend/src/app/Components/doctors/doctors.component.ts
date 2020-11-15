import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { User } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-doctors',
  templateUrl: './doctors.component.html',
  styleUrls: ['./doctors.component.css']
})
export class DoctorsComponent implements OnInit {
  formSearch: FormGroup;
  users: User[];
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.formSearch = new FormGroup({
      "search": new FormControl(null)
    });
    this.userService.getDoctors().subscribe(users => {
      this.users = users;
    });
  }

  submit(){
    const name =this.formSearch.get("search").value;
    this.userService.searchDoctor(name).subscribe(users => {
      this.users = users
    });
  }
}
