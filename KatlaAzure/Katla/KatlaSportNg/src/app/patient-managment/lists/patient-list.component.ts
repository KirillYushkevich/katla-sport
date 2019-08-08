import { Component, OnInit } from '@angular/core';
import { PatientItem } from '../models/patient-item';
import { PatientService } from '../services/patient.service';

@Component({
  selector: 'app-patient-list',
  templateUrl: './patient-list.component.html',
  styleUrls: ['./patient-list.component.css']
})
export class PatientListComponent implements OnInit {

  patients: PatientItem[];

  constructor(private PatientService: PatientService) { }

  ngOnInit() {
    this.getPatients();
  }

  getPatients() {
    this.PatientService.getPatients().subscribe(h => this.patients = h);
  }

  onDelete(Patientid: number) {
    this.PatientService.deletePatient(Patientid).subscribe(p => this.getPatients());   
  }
}
