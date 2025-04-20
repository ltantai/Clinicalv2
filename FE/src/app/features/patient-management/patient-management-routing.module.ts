import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { PatientManagementComponent } from './patient-management.component';
import { PatientDetailComponent } from './components/patient-detail/patient-detail.component';


const routes: Routes = [
  { path: '', component: PatientManagementComponent },
  { path: 'detail/:id', component: PatientDetailComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PatientManagementRoutingModule { }