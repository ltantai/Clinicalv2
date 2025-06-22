import { Component, EventEmitter, Input, Output } from '@angular/core';
import { Router } from '@angular/router';
import { DoctorService } from '@services/doctor.service';
import { ClinicalMessageService } from '@services/message.service';

@Component({
  selector: 'app-doctor-form',
  templateUrl: './doctor-form.component.html',
  styleUrl: './doctor-form.component.scss'
})
export class DoctorFormComponent {
  @Output() onCloseDialog = new EventEmitter();
  @Input() isEdit = false;
  @Input() formData: any = {
    name: "",
    phoneNumber: "",
    email: ""
  };

  visible = false;
  isFormRequried = false;
  constructor(
    private router: Router,
    private doctorService: DoctorService,
    private messageService: ClinicalMessageService
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

  resetValue() {
    this.formData = {
      name: "",
      phoneNumber: "",
      email: ""
    };
    if (!this.isEdit) {
      this.router.navigate(["/doctors"]);
    }
    this.onCloseDialog.emit(false);
  }

  onCancel() {
    this.resetValue();
  }

  isInvalid(value: any): boolean {
    return value === null || value === undefined || value === '';
  }

  validateFormData(data: any): boolean {
    for (let key in data) {
      if (this.isInvalid(data[key])) {
        console.log(`Invalid field: ${key}`);
        this.isFormRequried = true;
        return false;
      }
    }
    this.isFormRequried = true;
    return true;
  }

  onSave() {
    if (!this.validateFormData(this.formData)) return;
    this.doctorService.create(this.formData).subscribe({ 
      next: () => {
        this.resetValue();
        this.messageService.showSuccess("Thêm mới bác sĩ thành công");
      },
      error: (error: any) => {      
        this.messageService.showError("Thêm mới bác sĩ thất bại");
      }
    });
  }

  onEdit() {
    if (!this.validateFormData(this.formData)) return;
    this.doctorService.update(this.formData).subscribe({
      next: () => {
        this.resetValue();
        this.messageService.showSuccess("Cập nhật bác sĩ thành công");
      },
      error: (error: any) => {      
        this.messageService.showError("Cập nhật bác sĩ thất bại");
      }
    });
  }
}
