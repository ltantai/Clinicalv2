import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { formatDate } from '@angular/common';
import { PatientService } from '@services/patient.service';
import { ClinicalMessageService } from '@services/message.service';

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
    form: [{ medicinName: "", numberOfTimesPerDay: 0, numberOfPillsPerDose: 0 }]
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
    private patientService: PatientService,
    private messageService: ClinicalMessageService
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

          if (this.dataSource.patientPrescriptions && this.dataSource.patientPrescriptions.length > 0) {
            this.dataSource.patientPrescriptions = this.dataSource.patientPrescriptions.sort((a: any, b: any) => b.order - a.order);
          }
        },
        error: (error: any) => { }
      })
    }
  }

  loadPrescriptionDetail(order: number, patientId: number) {
    this.patientService.getPrescriptionDetail(order, patientId).subscribe({
      next: (data: any) => {
        this.preScriptionDetail = data;
      },
      error: () => { }
    });
  }

  formatTime(value: any) {
    if (value) {
      return formatDate(value, "dd-MM-yyyy hh:mm:ss", 'en-us');
    }
    return value;
  }

  onOpenDetail(preScription: any) {
    if (preScription) {
      this.prescriptionVisible = true;
      this.loadPrescriptionDetail(preScription.order, preScription.patientId);
    }
  }

  onHide(event: any) { 
    this.visible = event;
  }

  onEdit() {
    this.formData = {
      id: this.patientId,
      patientName: this.dataSource.patientName,
      gender: { value: this.dataSource.gender },
      age: Number(this.dataSource.age) ?? 0,
      address: this.dataSource.address,
      note: this.dataSource.note,
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
      form: [{ medicinName: "", numberOfTimesPerDay: 0, numberOfPillsPerDose: 0 }]
    };
    this.prescriptionFormVisible = false;
  }

  onCancel() {
    this.resetValue();
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
        this.messageService.showSuccess("Thêm đơn thuốc thành công");
      },
      error: (error: any) => { this.messageService.showError("Thêm đơn thuốc thất bại"); }
    });
  }
}
