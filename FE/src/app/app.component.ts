import { Component } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'clinical-app';
 
  constructor() {

  }

  isManagementMenuOpen = true;

  toggleManagementMenu() {
    this.isManagementMenuOpen = !this.isManagementMenuOpen;
  }
}
