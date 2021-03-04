import { animate, state, style, transition, trigger } from '@angular/animations';
import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTable } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/Models/UserModel';
import { PatientService } from 'src/app/Services/PatientService/patient.service';
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
    public dialog: MatDialog) {}


  /** Columns displayed in the table. Columns IDs can be added, removed, or reordered. */
  displayedColumns = ['firstName', 'age', 'email', 'phoneNumber'];

  ngOnInit() {
    this.dataSource = new DataTableDataSource(this.patientService);
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
        console.log(result);
    });
  }
}
