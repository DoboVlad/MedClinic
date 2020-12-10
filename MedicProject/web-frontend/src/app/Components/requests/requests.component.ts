import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {
  users: User[];
  constructor(public userService: UserService) { }

  ngOnInit(): void {
    this.userService.getUnapprovedUsers().subscribe(users => {
      this.users = users;
    })
  }
  acceptUser(id: number){
    this.userService.approveUser(id);
  }
}
