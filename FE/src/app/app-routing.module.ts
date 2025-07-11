import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  {
    path: 'patients',
    loadChildren: () =>
      import('./features/patient-management/patients-management.module').then(m => m.PatientManagementModule)
  },
  {
    path: 'doctors',
    loadChildren: () =>
      import('./features/doctor-management/doctor-management.module').then(m => m.DoctorManagementModule)
  },
  { path: '', redirectTo: '/patients', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
