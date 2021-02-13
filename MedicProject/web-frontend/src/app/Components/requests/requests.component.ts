import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from 'src/app/Services/account.service';
import { MedicService } from 'src/app/Services/MedicService/medic.service';

@Component({
  selector: 'app-requests',
  templateUrl: './requests.component.html',
  styleUrls: ['./requests.component.css']
})
export class RequestsComponent implements OnInit {
  users: User[];
  constructor(public accountService: AccountService,
    private medicService: MedicService) { }

  ngOnInit(): void {
    this.accountService.isFetching = true;
    this.medicService.getUnapprovedUsers().subscribe(users => {
      this.users = users;
      this.accountService.isFetching = false;
    })
  }
  acceptUser(id: number){
    this.medicService.approveUser(id);
  }
}
