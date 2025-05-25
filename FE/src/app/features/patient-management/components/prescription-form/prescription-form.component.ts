import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-prescription-form',
  templateUrl: './prescription-form.component.html',
  styleUrl: './prescription-form.component.scss'
})
export class PrescriptionFormComponent implements OnInit {
  @Input() prescriptionForm: any = [];
  @Input() addPrescriptionFormSpecification = false;

  isRequired = false;
  constructor() {

  }

  ngOnInit(): void {

  }

  isValidRow(item: any): boolean {
    return (
      item.medicineName &&
      item.numberOfTimesPerDay > 0 &&
      item.numberOfPillsPerDose > 0
    );
  }

  allRowsValid(): boolean {
    return this.prescriptionForm.every((row: any) => this.isValidRow(row));
  }

  addRow() {
    if (this.allRowsValid()) {
      this.prescriptionForm.push({
        medicineName: "",
        numberOfTimesPerDay: 0,
        numberOfPillsPerDose: 0
      });
      this.isRequired = false;
    } else {
      this.isRequired = true;
    }
  }

  removeRow(index: number) {
    if (this.addPrescriptionFormSpecification) {
      if (this.prescriptionForm.length > 1) {
        this.prescriptionForm.splice(index, 1);
      }
    }
    else {
      this.prescriptionForm.splice(index, 1);
    }
  }
}
