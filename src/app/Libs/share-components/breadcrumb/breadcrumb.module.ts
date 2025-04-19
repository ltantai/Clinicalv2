import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { BreadcrumbComponent } from './breadcrumb.component';
import { BreadcrumbModule } from 'primeng/breadcrumb';

@NgModule({
  declarations: [
    BreadcrumbComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    BreadcrumbModule
  ],
  providers: [],
  bootstrap: [],
  exports: [
    BreadcrumbComponent
  ]
})
export class BreadcrumbInfoModule { }
