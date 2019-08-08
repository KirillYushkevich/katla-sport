import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'environments/environment';
import { Observable } from 'rxjs';
import {PatientItem } from '../models/patient-item';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private url = environment.apiUrl + 'api/patient/';

  constructor(private http: HttpClient) { }

  getPatients(): Observable<Array<PatientItem>> {
    return this.http.get<Array<PatientItem>>(this.url);
  }

  getPatient(patientid: number): Observable<PatientItem> {
    return this.http.get<PatientItem>(`${this.url}${patientid}`);
  }

  addPatient(patient: PatientItem): Observable<PatientItem> {
    return this.http.post<PatientItem>(`${this.url}`, patient);
  }

  updatePatient(patient: PatientItem): Observable<Object> {
    return this.http.put<Object>(`${this.url}${patient.id}`, patient);
  }

  deletePatient(patientid: number): Observable<Object> {
    return this.http.delete<Object>(`${this.url}${patientid}`);
  }
}
