import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { Appointment } from 'src/app/Models/AppointmentModel';
import { Medicine } from 'src/app/Models/MedicineModel';
import { AppointmentService } from 'src/app/Services/AppointmentService/appointment.service';

@Component({
  selector: 'app-delete-appointment',
  templateUrl: './delete-appointment.component.html',
  styleUrls: ['./delete-appointment.component.css']
})
export class DeleteAppointmentComponent implements OnInit {
  editAppointmentForm: FormGroup;
  resultForm: FormGroup;
  hours: Appointment[];
  appointment: Appointment;
  minDate: Date;
  newDate: Date;
  medicineList: Medicine[] = [];

  constructor(private appointmentService: AppointmentService,
     private dialogRef: MatDialogRef<DeleteAppointmentComponent>,
     @Inject(MAT_DIALOG_DATA) public data: any, private router: Router) { }

  ngOnInit(): void {
    this.minDate = new Date();
    var date = new Date();
    this.newDate = new Date(date.setMonth(date.getMonth()+1));
    this.resultForm = new FormGroup({
      "result": new FormControl(null, Validators.required),
      "medicine": new FormControl(null),
      "dosage": new FormControl(null)
    });
    this.appointmentService.getAnAppoinmentById(this.data.id).subscribe(appointment => {
      this.appointment = appointment;
      this.editAppointmentForm = new FormGroup({
        'date': new FormControl(this.appointment[0].date, Validators.required),
        "hour": new FormControl(null, Validators.required)
      });
      this.getSchedule(this.appointment[0].date);
    })
  }

  getSchedule(data?: Date){
    var date: Date;
    if(data){
      date = data;
    }
    else {
      date = this.editAppointmentForm.value.date;
      const newDate = new Date(date);
      console.log(newDate);
    }
    this.appointmentService.getMedicSchedule(date).subscribe(hours => {
      this.hours = hours;
    });
  }

  onSubmit(){
    const date = {...this.editAppointmentForm.value};
    date.Id = this.data.id;
    this.appointmentService.updateAppointment(date).subscribe(result => {
      this.appointmentService.info = "Appointment was updated succesfully!";
      this.dialogRef.close();
    }, error => {
      console.log(error);
    });
  }

  addToList(){
    console.log(this.resultForm.get("medicine").value);
    let medicine: Medicine = {
      dosage: this.resultForm.get("dosage").value,
      medicine: this.resultForm.get("medicine").value
    };
    console.log(medicine);
    this.medicineList.push(medicine);
  }

  submitResult(){

  }

  weekendsDatesFilter = (d: Date): boolean => {
    const day = d.getDay();

    /* Prevent Saturday and Sunday for select. */
    return day !== 0 && day !== 6 ;
}

  onDelete(){
    this.appointmentService.deleteAppointmentById(this.data.id).subscribe(response => {
      this.appointmentService.info = "Appointment was deleted succesfully!";
      this.dialogRef.close();
    })
  }

  back(){
    this.router.navigate(["/doctorappointments"]);
  }
}
