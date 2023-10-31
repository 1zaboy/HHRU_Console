import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Resume } from 'src/app/common/models/resume';
import { ApiService } from 'src/app/common/services/api.service';

@Component({
  selector: 'app-resume-up',
  templateUrl: './resume-up.component.html',
  styleUrls: ['./resume-up.component.scss']
})
export class ResumeUpComponent implements OnInit {
  resumes: Resume[] = [];

  constructor(
    private readonly _api: ApiService,
  ) {
    //
  }

  get hasResumes(): boolean {
    return this.resumes.length > 0;
  }

  ngOnInit(): void {
    this._api.loadResumes().subscribe(x => this.resumes = x);
  }
}
