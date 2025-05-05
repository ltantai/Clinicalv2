import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { PatientService } from '../../../../Libs/Services/patient.service';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrl: './patient-form.component.scss'
})
export class PatientFormComponent implements OnInit {
  @Output() onCloseDialog = new EventEmitter();
  @Input() isEdit = false;
  @Input() formData: any = {
    patientName: "",
    gender: {value: "Nữ"},
    age: 1,
    address: "",
    diagnostic: {
      lowerLevel: "",
      department: ""
    },
    prescriptionForm: [
      {id: 0, medicineName: "", numberOfTimesPerDay: 0, numberOfPillsPerDose: 0, order: 0, patientId: 0}
    ],
    treatmentIndication: "",
    doctorName: ""
  };
  genders = [
    { value: 'Nam'},
    { value: 'Nữ'},
    { value: 'Khác'},
  ];
  patients: any[] = [
    {id: 1, name: "Lê Đạt Nhân"},
    {id: 2, name: "Lê Tấn Tài"}
  ];
  selectedPatient: any;

  constructor(
    private router: Router,
    private patientService: PatientService
  ) {
    
  }

  ngOnInit(): void {
    
  }

  preventDecimalInput(event: KeyboardEvent) {
    const invalidChars = ['.', ','];
    if (invalidChars.includes(event.key)) {
      event.preventDefault();
    }
  }

  onCancel() {

  }
  
  onSave() {
    console.log(this.formData);

    const form = {
      id: 0,
      patientName: this.formData.patientName,
      gender: this.formData.gender.value,
      age: this.formData.age,
      address: this.formData.address,
      lowerLevel: this.formData.diagnostic.lowerLevel,
      medicalTreatmentDepartment: this.formData.diagnostic.department,
      treatmentIndication: this.formData.treatmentIndication,
      doctorId: this.selectedPatient.id,
      patientPrescriptionInputModels: this.formData.prescriptionForm
    }

    this.patientService.createPatient(form).subscribe({
      next: () => {
        this.onCloseDialog.emit(false);
        this.router.navigate(["/patients"]);
      },
      error: (error: any) => {console.log(error);}
    });
  }
}
