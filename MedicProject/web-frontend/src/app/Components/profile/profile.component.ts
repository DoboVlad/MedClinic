import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User;
  constructor(private userService: UserService) { }

  ngOnInit(): void {
    this.userService.myAccount().subscribe(user => {
      this.user = user;
      if(parseInt(this.user.cnp.substr(0,1)) == 1 || parseInt(this.user.cnp.substr(0,1)) == 5){
        this.user.gender = "Male";
      }else {
        this.user.gender = "Female";
      }
    });
  }
}
