import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { Table } from 'primeng/table';
import { PatientService } from '../../Libs/Services/patient.service';

@Component({
  selector: 'app-patient-management',
  templateUrl: './patient-management.component.html',
  styleUrl: './patient-management.component.scss'
})
export class PatientManagementComponent implements OnInit {
  @ViewChild('dt') dt!: Table;
  searchValue = "";
  patients: any = [];
  visible = false;
  first = 0;
  rows = 10;
  totalRecords = 1;
  constructor(
    private router: Router,
    private patientService: PatientService
  ) { }

  ngOnInit(): void {
    //this.loadAllPatientData(this.searchValue, this.first + 1, this.rows);
  }

  loadPatientsLazy(event: any) {
    const page = event.first / event.rows;
    const size = event.rows;
    const search = this.searchValue;

    this.loadAllPatientData(search, page + 1, size);
  }
  
  loadAllPatientData(search: string, pageNumber: number, pageSize: number) {
    this.patientService.getAllPaginatedPatients(search, pageNumber, pageSize).subscribe({
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
          this.patients = data;
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
    this.loadAllPatientData(this.searchValue, this.first + 1, this.rows);
  }

  addNewPatient() {
    this.visible = true;
  }
}
