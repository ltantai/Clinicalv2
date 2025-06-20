import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoctorManagementComponent } from './doctor-management.component';
import { DoctorDetailComponent } from './components/doctor-detail/doctor-detail.component';


const routes: Routes = [
  { path: '', component: DoctorManagementComponent },
  { path: 'detail/:id', component: DoctorDetailComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DoctorManagementRoutingModule { }