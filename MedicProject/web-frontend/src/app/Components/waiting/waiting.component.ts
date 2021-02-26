import { Component, OnInit } from '@angular/core';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from 'src/app/Services/account.service';
import { MedicService } from 'src/app/Services/MedicService/medic.service';

@Component({
  selector: 'app-waiting',
  templateUrl: './waiting.component.html',
  styleUrls: ['./waiting.component.css']
})
export class WaitingComponent implements OnInit {
  medic: User;
  constructor(public accountService: AccountService,
    private medicService: MedicService) { }

  ngOnInit(): void {
    this.medicService.getMedicInfo().subscribe(medic => {
      this.medic = medic;
    })
  }

}
