import { Injectable } from '@angular/core';
import { HttpWrapperService } from './http-wrapper.service';

@Injectable({
  providedIn: 'root'
})
export class DoctorService {
  private baseUrl = 'https://localhost:7129/api/doctors';

  constructor(private http: HttpWrapperService) {}

  getAllDoctors(){
    return this.http.get<any>(`${this.baseUrl}/getall`);
  }

  getAllPaginatedDoctors(searchValue: string, pageNumber: number, pageSize: number){
    return this.http.get<any>(`${this.baseUrl}/GetPaginatedDoctors?search=${searchValue}&pageNumber=${pageNumber}&pageSize=${pageSize}`);
  }

  getById(id: number) {
    return this.http.get<any>(`${this.baseUrl}/GetDoctorById/${id}`);
  }

  create(formData: any) {
    return this.http.post(`${this.baseUrl}/add`, formData);
  }

  update(formData: any){
    return this.http.put(`${this.baseUrl}/update`, formData);
  }

  delete(id: number) {
    return this.http.delete(`${this.baseUrl}/delete/${id}`);
  }
}
