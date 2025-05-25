import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { ButtonModule } from 'primeng/button';
import { TableModule } from 'primeng/table';
import { BreadcrumbModule } from 'primeng/breadcrumb';
import { TooltipModule } from 'primeng/tooltip';
import { InputTextModule } from 'primeng/inputtext';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { TagModule } from 'primeng/tag';
import { FormsModule } from '@angular/forms';
import { PatientManagementRoutingModule } from './patient-management-routing.module';
import { PatientManagementComponent } from './patient-management.component';
import { CommonModule } from '@angular/common';
import { PatientDetailComponent } from './components/patient-detail/patient-detail.component';
import { PatientFormComponent } from './components/patient-form/patient-form.component';
import { BreadcrumbInfoModule } from '../../Libs/share-components/breadcrumb/breadcrumb.module';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { PrescriptionFormComponent } from './components/prescription-form/prescription-form.component';
import { InputTextareaModule } from 'primeng/inputtextarea';
@NgModule({
  declarations: [
    PatientManagementComponent,
    PatientDetailComponent,
    PatientFormComponent,
    PrescriptionFormComponent
  ],
  imports: [
    CommonModule,
    PatientManagementRoutingModule,
    ButtonModule,
    TableModule,
    TooltipModule,
    InputTextModule,
    IconFieldModule,
    InputIconModule,
    TagModule,
    FormsModule,
    BreadcrumbInfoModule,
    DialogModule,
    DropdownModule,
    InputTextareaModule
],
  providers: [],
  bootstrap: []
})
export class PatientManagementModule { }
