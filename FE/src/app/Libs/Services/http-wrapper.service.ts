import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, finalize, Observable, throwError } from 'rxjs';
import { LoadingService } from './loading.service';

@Injectable({
  providedIn: 'root'
})
export class HttpWrapperService {
  constructor(
    private http: HttpClient,
    private loadingService: LoadingService
  ) {}

  private defaultHeaders(): HttpHeaders {
    return new HttpHeaders({ 'Content-Type': 'application/json' });
  }

  get<T>(url: string, params?: HttpParams): Observable<T> {
    this.loadingService.show();
    return this.http.get<T>(url, {
      headers: this.defaultHeaders(),
      params
    }).pipe(
      catchError(this.handleError),
      finalize(() => this.loadingService.hide())
    );
  }

  post<T>(url: string, body: any): Observable<T> {
    this.loadingService.show();
    return this.http.post<T>(url, body, {
      headers: this.defaultHeaders()
    }).pipe(
      catchError(this.handleError),
      finalize(() => this.loadingService.hide())
    );
  }

  put<T>(url: string, body: any): Observable<T> {
    this.loadingService.show();
    return this.http.put<T>(url, body, {
      headers: this.defaultHeaders()
    }).pipe(
      catchError(this.handleError),
      finalize(() => this.loadingService.hide())
    );
  }

  delete<T>(url: string): Observable<T> {
    this.loadingService.show();
    return this.http.delete<T>(url, {
      headers: this.defaultHeaders()
    }).pipe(
      catchError(this.handleError),
      finalize(() => this.loadingService.hide())
    );
  }

  private handleError(error: any): Observable<never> {
    console.error('Http Error:', error);
    return throwError(() => error);
  }
}
