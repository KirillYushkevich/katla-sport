import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PatientService } from '../services/patient.service';
import { PatientItem } from '../models/patient-item'

@Component({
  selector: 'app-patient-form',
  templateUrl: './patient-form.component.html',
  styleUrls: ['./patient-form.component.css']
})
export class PatientFormComponent implements OnInit {

  file: File = null;
  patient = new PatientItem(0, "", "", 0, null,0);
  existed = false;
  patientslist: PatientItem[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private PatientService: PatientService

  ) { }

  ngOnInit() {
    this.PatientService.getPatients().subscribe(c => this.patientslist = c);
    this.route.params.subscribe(p => {
      if (p['id'] === undefined) return;
      this.PatientService.getPatient(p['id']).subscribe(h => this.patient = h);
      this.existed = true;
    });
  }

  onFileChange(event) {
      this.file = event.target.files[0];
      console.log(this.file);
  }

  navigateToPatients() {
    this.router.navigate(['/patients']);
  }

  onCancel() {
    this.navigateToPatients();
  }
  
  onSubmit() {
    if (this.existed) {
      this.PatientService.updatePatient(this.patient).subscribe(c => this.navigateToPatients());
    } else {
      this.PatientService.addPatient(this.patient).subscribe(c => this.navigateToPatients());
    }
  }
}
