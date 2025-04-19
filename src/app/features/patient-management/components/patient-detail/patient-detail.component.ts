import { Component, OnInit } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-patient-detail',
  templateUrl: './patient-detail.component.html',
  styleUrl: './patient-detail.component.scss'
})
export class PatientDetailComponent implements OnInit {
 items: MenuItem[] = [
    { label: 'Quản lý bệnh nhân',
      route: '/patients'
    }, 
    { label: 'Thông tin bệnh nhân' }
  ];

  dataSource = {
    patientName: "Nguyễn Văn A",
    gender: "Nam",
    age: 18,
    address: "Ấp 2, Tân Lộc, Tam Bình, Vĩnh Long",
    diagnostic: {
      lowerLevel: "Trạm y tế xã Tân Lộc",
      department: "Tai - mũi - họng"
    },
    treatmentIndication: "Uống thuốc 3lần/ Ngày",
    doctorName: "Nguyễn Đạt Nhân"
  }

  constructor() {
  }

  ngOnInit(): void {
    
  }
}
