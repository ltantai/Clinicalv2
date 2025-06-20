import { Injectable } from '@angular/core';
import { MessageService } from 'primeng/api';

@Injectable({
  providedIn: 'root'
})
export class ClinicalMessageService {

  constructor(private messageService: MessageService) { }

  showSuccess(message: string) {
    this.messageService.add({ severity: 'success', summary: 'Success', detail: message, key: 'br', life: 3000  });
  }

  showInfo(message: string) {
    this.messageService.add({ severity: 'info', summary: 'Info', detail: message, key: 'br', life: 3000  });
  }

  showWarn(message: string) {
    this.messageService.add({ severity: 'warn', summary: 'Warn', detail: message, key: 'br', life: 3000 });
  }

  showError(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Error', detail: message, key: 'br', life: 3000 });
  }

}
