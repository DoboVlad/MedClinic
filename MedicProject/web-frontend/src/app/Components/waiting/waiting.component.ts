import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { UserService } from 'src/app/Services/user.service';

@Component({
  selector: 'app-waiting',
  templateUrl: './waiting.component.html',
  styleUrls: ['./waiting.component.css']
})
export class WaitingComponent implements OnInit {
  medic: User;
  constructor(public userService: UserService) { }

  ngOnInit(): void {
    this.userService.medicAccount().subscribe(medic => {
      this.medic = medic;
    })
  }

}
