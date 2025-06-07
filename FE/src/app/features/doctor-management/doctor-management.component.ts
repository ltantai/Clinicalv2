import { Component, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Table } from 'primeng/table';
import { DoctorService } from '../../Libs/Services/doctor.service';

@Component({
  selector: 'app-doctor-management',
  templateUrl: './doctor-management.component.html',
  styleUrl: './doctor-management.component.scss'
})
export class DoctorManagementComponent {
@ViewChild('dt') dt!: Table;
  searchValue = "";
  doctors: any = [];
  visible = false;
  first = 0;
  rows = 10;
  totalRecords = 1;
  constructor(
    private router: Router,
    private doctorService: DoctorService
  ) { }

  ngOnInit(): void {}

  loadDoctorsLazy(event: any) {
    const page = event.first / event.rows;
    const size = event.rows;
    const search = this.searchValue;

    this.loadAllDoctorData(search, page + 1, size);
  }
  
  loadAllDoctorData(search: string, pageNumber: number, pageSize: number) {
    this.doctorService.getAllPaginatedDoctors(search, pageNumber, pageSize).subscribe({
      next: (results: any) => {
        if (results.items) {
          const data = results.items.map((item: any, index: number) => ({
            id: item.id,
            order: index + 1,
            patientName: item.patientName,
            gender: item.gender,
            address: item.address,
            doctorName: item.doctor ? item.doctor.name : ""
          }));
          this.doctors = data;
          this.totalRecords = results.totalCount;
        }
      },
      error: (error: any) => { console.log(error); }
    });
  }

  onGlobalFilter(event: Event) {
    const value = (event.target as HTMLInputElement)?.value;
    this.dt.filterGlobal(value, 'contains');
  }

  pageChange(event: any) {
    this.first = event.first;
    this.rows = event.rows;
  }

  onOpenDetail(patient: any) {
    this.router.navigate([`/patients/detail/${patient.id}`]);
  }

  onHide(event: any) {
    this.visible = event;
    this.loadAllDoctorData(this.searchValue, this.first + 1, this.rows);
  }

  add() {
    this.visible = true;
  }
}
