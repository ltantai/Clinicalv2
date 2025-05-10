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
  // @Input() set data(value: any) {
  //   if (value) {
  //     this.formData = {
  //       patientName: value.patientName,
  //       gender: value.gender,
  //       age: value.age,
  //       address: value.address,
  //       diagnostic: value.diagnostic,
  //       prescriptionForm: value.prescriptionForm,
  //       treatmentIndication: value.treatmentIndication,
  //       doctorId: value.doctorId
  //     };

  //     if (this.formData.doctorId) {}
  //   }
  // }

  @Input() formData: any = {
    patientName: "",
    gender: { value: "Nữ" },
    age: 1,
    address: "",
    diagnostic: {
      lowerLevel: "",
      department: ""
    },
    prescriptionForm: [
      { id: 0, medicineName: "", numberOfTimesPerDay: 0, numberOfPillsPerDose: 0, order: 0, patientId: 0 }
    ],
    treatmentIndication: "",
    doctorId: 0
  };

  genders = [
    { value: 'Nam' },
    { value: 'Nữ' },
    { value: 'Khác' },
  ];

  doctors: any[] = [
    { id: 1, name: "L1" },
    { id: 2, name: "Lê Đạt Nhân" }
  ];

  selectedDoctor: any;

  constructor(
    private router: Router,
    private patientService: PatientService
  ) {

  }

  ngOnInit(): void {
    this.setDoctorFieldValue();
  }

  setDoctorFieldValue () {
    if (this.formData.doctorId > 0) {
      this.selectedDoctor = this.doctors.find((item: any) => item.id === this.formData.doctorId);
    }
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
    const form = {
      id: 0,
      patientName: this.formData.patientName,
      gender: this.formData.gender.value,
      age: this.formData.age,
      address: this.formData.address,
      lowerLevel: this.formData.diagnostic.lowerLevel,
      medicalTreatmentDepartment: this.formData.diagnostic.department,
      treatmentIndication: this.formData.treatmentIndication,
      doctorId: this.selectedDoctor.id,
      patientPrescriptionInputModels: this.formData.prescriptionForm
    }

    this.patientService.createPatient(form).subscribe({
      next: () => {
        this.onCloseDialog.emit(false);
        this.router.navigate(["/patients"]);
      },
      error: (error: any) => { console.log(error); }
    });
  }
}
