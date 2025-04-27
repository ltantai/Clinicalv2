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

  getPatientById(id: number) {
    return this.http.get<any>(`${this.baseUrl}/${id}`);
  }

  createPatient(patientData: any) {
    return this.http.post(`${this.baseUrl}`, patientData);
  }

  updatePatient(id: number, patientData: any){
    return this.http.put(`${this.baseUrl}/${id}`, patientData);
  }

  deletePatient(id: number) {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
