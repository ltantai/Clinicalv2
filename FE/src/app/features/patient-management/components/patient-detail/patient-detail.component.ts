import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { PatientService } from '../../../../Libs/Services/patient.service';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrl: './patient-detail.component.scss'
})
export class PatientDetailComponent implements OnInit {
  visible = false;
  prescriptionVisible = false;
  prescriptionFormVisible = false;
  prescriptionForm: any = {
    note: "",
    form:[{medicinName: "", numberOfTimesPerDay: 0, numberOfPillsPerDose: 0}]
  };
  items: MenuItem[] = [
    {
      label: 'Quản lý bệnh nhân',
      route: '/patients'
    },
    { label: 'Thông tin bệnh nhân' }
  ];

  dataSource: any = {};
  formData: any = {};
  preScriptionDetail: any[] = [];
  patientId = 0;

  constructor(
    private route: ActivatedRoute,
    private patientService: PatientService
  ) {
  }

  ngOnInit(): void {
    this.loadPatientDetail(); 
  }

  loadPatientDetail() {
    this.patientId = Number(this.route.snapshot.paramMap.get("id")) ?? 0;
    if (this.patientId) {
      const id = Number(this.patientId) ?? 0;
      this.patientService.getPatientById(id).subscribe({
        next: (data: any) => {
          this.dataSource = data;
        }, 
        error: (error: any) => {}
      })
    }
  }

  loadPrescriptionDetail(order: number, patientId: number) {
    this.patientService.getPrescriptionDetail(order, patientId).subscribe({
      next: (data: any) => {
        this.preScriptionDetail = data;        
      },
      error: () => {}
    });
  }

  formatTime(value: any) {
    if (value) {
      return formatDate(value, "dd-MM-yyyy hh:mm:ss",'en-us');
    }
    return value;
  }

  onOpenDetail(preScription: any) {
    if (preScription) {
      this.prescriptionVisible = true;
      this.loadPrescriptionDetail(preScription.order, preScription.patientId);      
    }
  }

  onHide() {}

  onEdit() {
    this.formData = {
      patientName: this.dataSource.patientName,
      gender: {value: this.dataSource.gender},
      age: this.dataSource.age,
      address: this.dataSource.address,
      diagnostic: {
        lowerLevel: this.dataSource.lowerLevel,
        department: this.dataSource.medicalTreatmentDepartment
      },
      prescriptionForm: [],
      treatmentIndication: this.dataSource.treatmentIndication,
      doctorId: this.dataSource.doctor.id
    }
    this.visible = true;
  }

  addPrescription() {
    this.prescriptionFormVisible = true;
  }

  resetValue() {
    this.prescriptionForm = {
      note: "",
      form:[{medicinName: "", numberOfTimesPerDay: 0, numberOfPillsPerDose: 0}]
    };
    this.prescriptionFormVisible = false;
  }

  onCancel() {

  }

  onSave() {
    const form = {
      id: 0,
      patientId: this.patientId,
      note: this.prescriptionForm.note,
      patientPrescriptionInputModels: this.prescriptionForm.form
    }    

    this.patientService.addPrescriptionForPatient(form).subscribe({
      next: () => {
        this.resetValue();
        this.loadPatientDetail(); 
      },
      error: (error: any) => { console.log(error); }
    });
  }
}
