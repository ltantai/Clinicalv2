import { Component, OnInit, viewChild, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { Table } from 'primeng/table';
import { ClinicalMessageService } from '@services/message.service';
import { PatientService } from '@services/patient.service';
import { PatientFormComponent } from './components/patient-form/patient-form.component';

@Component({
  selector: 'app-patient-management',
  templateUrl: './patient-management.component.html',
  styleUrl: './patient-management.component.scss'
})
export class PatientManagementComponent implements OnInit {
  @ViewChild('dt') dt!: Table;
  @ViewChild('patientForm') patientForm!: PatientFormComponent
  searchValue = "";
  patients: any = [];
  visible = false;
  first = 0;
  rows = 10;
  totalRecords = 1;


  constructor(
    private router: Router,
    private patientService: PatientService,
    private messageService: ClinicalMessageService
  ) { }

  ngOnInit(): void {}

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
            order: pageNumber > 1 ? ((pageNumber*10) + (index + 1)) : (index + 1),
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

  onDelete(patient: any) {
    this.patientService.deletePatient(patient.id).subscribe({
      next: () => {
          this.loadAllPatientData(this.searchValue, this.first + 1, this.rows);
          this.messageService.showSuccess("Đã xóa bệnh nhân thành công");
      },
      error: () => {this.messageService.showError("Xóa bệnh nhân thất bại");}
    })
  }

  onHide() {
    if (!this.patientForm) return;
    this.visible = false;
    this.patientForm.resetValue();
  }

  onCloseDialog(event: any) {
    this.visible = event;
    this.loadAllPatientData(this.searchValue, this.first + 1, this.rows);
  }

  addNewPatient() {
    this.visible = true;
  }
}
