import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { DropdownModule } from 'primeng/dropdown';
import { IconFieldModule } from 'primeng/iconfield';
import { InputIconModule } from 'primeng/inputicon';
import { InputTextModule } from 'primeng/inputtext';
import { InputTextareaModule } from 'primeng/inputtextarea';
import { TableModule } from 'primeng/table';
import { TagModule } from 'primeng/tag';
import { TooltipModule } from 'primeng/tooltip';
import { BreadcrumbInfoModule } from '../../Libs/share-components/breadcrumb/breadcrumb.module';
import { DoctorManagementRoutingModule } from './doctor-management-routing.module';
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    DoctorManagementRoutingModule,
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
export class DoctorManagementModule { }
