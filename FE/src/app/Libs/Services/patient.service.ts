import { Injectable } from '@angular/core';
import { HttpWrapperService } from './http-wrapper.service';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private baseUrl = 'https://localhost:7129/api/patients';

  constructor(private http: HttpWrapperService) {}

  getAllPatients(){
    return this.http.get<any>(`${this.baseUrl}/getall`);
  }

  getAllPaginatedPatients(searchValue: string, pageNumber: number, pageSize: number){
    return this.http.get<any>(`${this.baseUrl}/GetPaginatedPatients?search=${searchValue}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getPatientById(id: number) {
    return this.http.get<any>(`${this.baseUrl}/GetPatientById/${id}`);
  }

  getPrescriptionDetail(order: number, patientId: number) {
    return this.http.get<any>(`${this.baseUrl}/GetPrescriptionDetail?order=${order}&patientId=${patientId}`);
  }

  createPatient(patientData: any) {
    return this.http.post(`${this.baseUrl}/add`, patientData);
  }

  updatePatient(patientData: any){
    return this.http.put(`${this.baseUrl}/update`, patientData);
  }

  deletePatient(id: number) {
    return this.http.delete(`${this.baseUrl}/delete/${id}`);
  }

  addPrescriptionForPatient(formData: any) {
    return this.http.post(`${this.baseUrl}/AddPresciptionForPatient`, formData);
  }
}
