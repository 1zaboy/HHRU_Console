import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { Resume } from 'src/app/common/models/resume';
import { ApiService } from 'src/app/common/services/api.service';

@Component({
  selector: 'app-resume-up-item',
  templateUrl: './resume-up-item.component.html',
  styleUrls: ['./resume-up-item.component.scss']
})
export class ResumeUpItemComponent implements OnInit {
  @Input() resume: Resume;

  resumeValue = new FormControl<boolean>(false);

  constructor(
    private readonly _api: ApiService,
  ) { }

  ngOnInit(): void {
    this.resumeValue.setValue(this.resume.isAdvancing)
    this.resumeValue.valueChanges
      .subscribe(x => {
        console.log(x);

        this._api.setAdvancing(this.resume.id, x).subscribe();
      });
  }
}
