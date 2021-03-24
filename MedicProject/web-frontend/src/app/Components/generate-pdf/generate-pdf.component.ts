import { Component, Inject, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from 'src/app/Models/UserModel';
import { Pdf } from 'src/app/Models/PdfModel';
import { PatientService } from 'src/app/Services/PatientService/patient.service';

@Component({
  selector: 'app-generate-pdf',
  templateUrl: './generate-pdf.component.html',
  styleUrls: ['./generate-pdf.component.css']
})
export class GeneratePdfComponent implements OnInit {
  user: User;
  generatePdfForm: FormGroup;
  pdf: Pdf;
  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
   private patientService: PatientService, private dialogRef: MatDialogRef<GeneratePdfComponent>) { }

  ngOnInit(): void {
    this.generatePdfForm = new FormGroup({
      'sendTo': new FormControl(null, Validators.required),
      'reason': new FormControl(null, Validators.required),
      'diagnostic': new FormControl(null),
      'treatment': new FormControl(null)
    });
  }

  onSubmit(){
    this.pdf = {...this.generatePdfForm.value};
    this.pdf.id = this.data.user.id;
    this.patientService.pdfData = this.pdf;
    this.dialogRef.close();
  }

}
