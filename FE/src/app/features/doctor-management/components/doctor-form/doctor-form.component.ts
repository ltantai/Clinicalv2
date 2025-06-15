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
    this.router.navigate(["/doctors"]);
  }

  onCancel() {
    this.resetValue();
  }

  onSave() {
    const form = {
      id: 0,
      name: this.formData.name,
      phoneNumber: this.formData.phoneNumber,
      email: this.formData.email,
    }

    console.log(form);
    

    this.doctorService.create(form).subscribe({
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
    const form = {
      id: this.formData.id,
      name: this.formData.name,
      phoneNumber: this.formData.phoneNumber,
      email: this.formData.email,
    }
    this.doctorService.update(form).subscribe({
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
