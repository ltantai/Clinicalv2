import { AfterViewInit, ChangeDetectorRef, Component } from '@angular/core';
import { LoadingService } from './Libs/Services/loading.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements AfterViewInit{
  title = 'clinical-app';
  isManagementMenuOpen = true;

  constructor(
    public loadingService: LoadingService,
    private cdRef: ChangeDetectorRef
  ) {
  }

  ngAfterViewInit(): void {
    this.loadingService.loading$.subscribe(() => {
      this.cdRef.detectChanges();
    });
  }
  toggleManagementMenu() {
    this.isManagementMenuOpen = !this.isManagementMenuOpen;
  }
}
