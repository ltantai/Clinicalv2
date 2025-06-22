import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from '@services/doctor.service';
import { ClinicalMessageService } from '@services/message.service';
import { PatientService } from '@services/patient.service';
import { PrescriptionFormComponent } from '../prescription-form/prescription-form.component';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrl: './patient-form.component.scss'
})
export class PatientFormComponent implements OnInit {
  @ViewChild("prescriptionForm") prescriptionForm !: PrescriptionFormComponent
  @Output() onCloseDialog = new EventEmitter();
  @Input() isEdit = false;
  @Input() formData: any = {
    patientName: "",
    gender: { value: "Nữ" },
    age: 1,
    address: "",
    diagnostic: {
      lowerLevel: "",
      department: ""
    },
    prescriptionForm: [],
    treatmentIndication: "",
    doctorId: 0,
    note: ""
  };

  genders = [
    { value: 'Nam' },
    { value: 'Nữ' },
    { value: 'Khác' },
  ];

  doctors: any[] = [];
  doctorsLookupData: any[] = [];

  selectedDoctor: any;
  isFormRequried = false;
  constructor(
    private router: Router,
    private patientService: PatientService,
    private doctorService: DoctorService,
    private messageService: ClinicalMessageService
  ) {

  }

  ngOnInit(): void {
    this.lookupDoctors();
  }

  lookupDoctors() {
    this.doctorService.getAllDoctors().subscribe({
      next: (data: any) => {
        this.doctors = [...data];
        this.doctorsLookupData = [...data];
        this.setDoctorFieldValue();
      },
      error: () => {}
    })
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

  resetValue() {
    this.formData = {  patientName: "",  gender: { value: "Nữ" },  age: 1, address: "",
      diagnostic: {
        lowerLevel: "",
        department: ""
      },
      prescriptionForm: [],
      treatmentIndication: "", doctorId: 0, note: ""
    };
    this.genders = [
      { value: 'Nam' },
      { value: 'Nữ' },
      { value: 'Khác' },
    ];

    this.doctors = [...this.doctorsLookupData];
    this.selectedDoctor = null;
    this.isFormRequried = false;
    this.onCloseDialog.emit(false);
    if (!this.isEdit) {
      this.router.navigate(["/patients"]);
    }
  }

  onCancel() {
    this.resetValue();
  }

  isInvalid(value: any): boolean {
    return value === null || value === undefined || value === '';
  }

  validateFormData(data: any): boolean {
    let formValid = true;
    if (this.prescriptionForm && !this.prescriptionForm.validation()) {
      this.isFormRequried = true;
      formValid = false;
    }
    for (let key in data) {
      if (this.isInvalid(data[key])) {
        this.isFormRequried = true;
        formValid = false;;
      }
    }
    return formValid;
  }

  setFormData() {
    const formData: any = {
      id: 0,
      patientName: this.formData.patientName,
      gender: this.formData.gender.value,
      age: this.formData.age,
      address: this.formData.address,
      lowerLevel: this.formData.diagnostic.lowerLevel,
      medicalTreatmentDepartment: this.formData.diagnostic.department,
      treatmentIndication: this.formData.treatmentIndication,
      doctorId: this.selectedDoctor ? this.selectedDoctor.id : null,
      patientPrescriptionInputModels: this.formData.prescriptionForm
    }
    if (this.isEdit) {
      formData.id = this.formData.id;
    }
    return formData;
  }

  onSave() {
    const form: any = this.setFormData();
    if (!this.validateFormData(form)) return; 
    form.note = this.formData.note;
    this.patientService.createPatient(form).subscribe({
      next: () => {
        this.resetValue();
        this.messageService.showSuccess("Thêm mới bệnh nhân thành công");
      },
      error: (error: any) => {      
        this.messageService.showError("Thêm mới bệnh nhân thất bại");
      }
    });
  }

  onEdit() {
    const form: any = this.setFormData();
    if (!this.validateFormData(form)) return; 
    form.note = this.formData.note;
    this.patientService.updatePatient(form).subscribe({
      next: () => {
        this.resetValue();
        this.messageService.showSuccess("Cập nhật bệnh nhân thành công");
      },
      error: (error: any) => {      
        this.messageService.showError("Cập nhật bệnh nhân thất bại");
      }
    });
  }
}
