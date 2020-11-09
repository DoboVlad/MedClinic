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
    });
  }

}
