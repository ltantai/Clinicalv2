import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Table } from 'primeng/table';

@Component({
  selector: 'app-patient-management',
  templateUrl: './patient-management.component.html',
  styleUrl: './patient-management.component.scss'
})
export class PatientManagementComponent implements OnInit{
  @ViewChild('dt') dt!: Table; 
  searchValue = "";
  products = [
    {id: 1, order: 1,patientName: "Nguyễn Văn A",gender: "Nam",address: "Ấp 2, Tân lộc, Tam Bình, Vĩnh Long", doctorName: "Nguyễn Đạt Nhân"},
    {id: 2, order: 2,patientName: "Nguyễn Thị B",gender: "Nữ",address: "Ấp 9, Tân lộc, Tam Bình, Vĩnh Long", doctorName: "Nguyễn Đạt Nhân"},
    {id: 3, order: 3,patientName: "Huỳnh Thanh Long",gender: "Nam",address: "Ấp 9, Tân lộc, Tam Bình, Vĩnh Long", doctorName: "Nguyễn Đạt Nhân"},
  ];

  visible = false;

  constructor(
    private router: Router
  ) {}

  ngOnInit(): void {
    
  }

  onGlobalFilter(event: Event) {
    const value = (event.target as HTMLInputElement)?.value;
    this.dt.filterGlobal(value, 'contains');
  }

  onOpenDetail(patient: any) {
    this.router.navigate([`/patients/detail/${patient.id}`]);
  }

  onHide(event: any) {
    this.visible = event;
  }

  addNewPatient() {
    this.visible = true;
  }
}
