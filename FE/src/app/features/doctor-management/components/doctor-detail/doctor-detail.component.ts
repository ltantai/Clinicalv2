import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { formatDate } from '@angular/common';
import { ClinicalMessageService } from '@services/message.service';
import { DoctorService } from '@services/doctor.service';

@Component({
  selector: 'app-doctor-detail',
  templateUrl: './doctor-detail.component.html',
  styleUrl: './doctor-detail.component.scss'
})
export class DoctorDetailComponent implements OnInit {
  visible = false;  
  items: MenuItem[] = [
    {
      label: 'Quản lý bác sĩ',
      route: '/doctors'
    },
    { label: 'Thông tin bác sĩ' }
  ];

  dataSource: any = {};
  formData: any = {};
  preScriptionDetail: any[] = [];
  doctorId = 0;

  constructor(
    private route: ActivatedRoute,
    private doctorService: DoctorService,
    private messageService: ClinicalMessageService
  ) {
  }

  ngOnInit(): void {
    this.loadDoctorDetail();
  }

  loadDoctorDetail() {
    this.doctorId = Number(this.route.snapshot.paramMap.get("id")) ?? 0;
    if (this.doctorId) {
      const id = Number(this.doctorId) ?? 0;
      this.doctorService.getById(id).subscribe({
        next: (data: any) => {
          this.dataSource = data;
        },
        error: (error: any) => { }
      })
    }
  }

  formatTime(value: any) {
    if (value) {
      return formatDate(value, "dd-MM-yyyy hh:mm:ss", 'en-us');
    }
    return value;
  }

  onHide(event: any) { 
    this.visible = event;
  }

  onEdit() {
    this.formData = {
      id: this.doctorId,
      name: this.dataSource.name,
      phoneNumber: this.dataSource.phoneNumber,
      email: this.dataSource.email,
      description: this.dataSource.description
    }
    this.visible = true;
  }

  resetValue() {
  }

  onCancel() {
  }
}
