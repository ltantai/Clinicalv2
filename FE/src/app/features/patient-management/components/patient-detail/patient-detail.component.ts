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

  constructor(
    private route: ActivatedRoute,
    private patientService: PatientService
  ) {
  }

  ngOnInit(): void {
    this.loadPatientDetail(); 
  }

  loadPatientDetail() {
    const patientId = this.route.snapshot.paramMap.get("id") ?? "";
    if (patientId) {
      const id = Number(patientId) ?? 0;
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
    
  }
}
