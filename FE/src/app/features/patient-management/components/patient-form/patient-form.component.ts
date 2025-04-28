import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrl: './patient-form.component.scss'
})
export class PatientFormComponent implements OnInit {
  @Output() onCloseDialog = new EventEmitter();
  @Input() isEdit = false;
  @Input() formData: any = {
    patientName: "",
    gender: {value: ""},
    age: 1,
    address: "",
    diagnostic: {
      lowerLevel: "",
      department: ""
    },
    prescriptionForm: [
      {medicineName: "", numberOfTimesPerDay: 0, numberOfPillsPerDose: 0, isDone: false}
    ],
    treatmentIndication: "",
    doctorName: ""
  };

  genders = [
    { value: 'Nam'},
    { value: 'Nữ'},
    { value: 'Khác'},
];
  
  constructor(
    private router: Router
  ) {
    
  }

  ngOnInit(): void {
    
  }

  preventDecimalInput(event: KeyboardEvent) {
    const invalidChars = ['.', ','];
    if (invalidChars.includes(event.key)) {
      event.preventDefault();
    }
  }

  onCancel() {

  }
  
  onSave() {
    console.log(this.formData);
    
    this.onCloseDialog.emit(false);
    this.router.navigate(["/patients"]);
  }
}
