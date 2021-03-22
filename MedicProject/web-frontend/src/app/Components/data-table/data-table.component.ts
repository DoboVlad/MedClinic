import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/Models/UserModel';
import { AccountService } from 'src/app/Services/account.service';
import { PatientService } from 'src/app/Services/PatientService/patient.service';
import { ApproveAccountComponent } from '../approve-account/approve-account.component';
import { DeletePatientComponent } from '../delete-patient/delete-patient.component';
import { UpdateAccountComponent } from '../update-account/update-account.component';
import { DataTableDataSource } from './data-table-datasource';



@Component({
  selector: 'app-data-table',
  templateUrl: './data-table.component.html',
  styleUrls: ['./data-table.component.css'],
  animations: [
    trigger('detailExpand', [
      state('collapsed', style({height: '0px', minHeight: '0'})),
      state('expanded', style({height: '*'})),
      transition('expanded <=> collapsed', animate('225ms cubic-bezier(0.4, 0.0, 0.2, 1)')),
    ]),
  ],
})
export class DataTableComponent implements AfterViewInit, OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  @ViewChild(MatSort) sort: MatSort;
  @ViewChild(MatTable) table: MatTable<User>;
  dataSource: DataTableDataSource;
  expandedElement: any;

  constructor(private patientService: PatientService,
    private toastr: ToastrService,
    public dialog: MatDialog, private accountService: AccountService) {}


  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['firstName', 'age', 'email', 'phoneNumber'];

  ngOnInit() {
    this.dataSource = new DataTableDataSource(this.patientService, this.accountService);
  }

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }


  openDialog(element?: any){
    const dialogRef = this.dialog.open(UpdateAccountComponent, {
      data: {user: element}
    });
      dialogRef.afterClosed().subscribe(result => {
        this.editAccount();
    });
  }


  approveDialog(element?: any){
    const dialogRef = this.dialog.open(ApproveAccountComponent, {
      data: {user: element}
    });
      dialogRef.afterClosed().subscribe(result => {
        this.editAccount();
    });
  }

  editAccount(){
    if(this.accountService.info != null){
      this.toastr.info(this.accountService.info);
      this.refreshTable();
    }
    this.accountService.info = null;
  }

  generatePdf(element){
    this.patientService.generatePdf(element.id).subscribe(pdf => {
      window.open('https://localhost:5001/api/pdf/generatePDF/3', '_blank');
    })
  }


  refreshTable(){
    this.dataSource = null;
    this.dataSource = new DataTableDataSource(this.patientService, this.accountService);
    this.dataSource.sort = this.sort;
    this.dataSource.paginator = this.paginator;
    this.table.dataSource = this.dataSource;
  }

  deletePatient(element){
    const dialogRef = this.dialog.open(DeletePatientComponent, {
      data: {user: element}
    });

    dialogRef.afterClosed().subscribe(result => {
      this.editAccount();
    });
  }
}
