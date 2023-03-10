import { HttpClient, HttpEventType, HttpErrorResponse } from '@angular/common/http';
import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { environment } from 'src/app/Environments/myEnvironment';

@Component({
  selector: 'upload-image',
  templateUrl: './upload-image.component.html',
  styleUrls: ['./upload-image.component.scss']
})
export class UploadImageComponent implements OnInit {
progress!: number;
message!: string;
@Output() public onUploadFinished = new EventEmitter();

constructor(private http: HttpClient) { }
ngOnInit() {
}
uploadFile = (files: any) => {
  if (files.length === 0) {
    return;
  }
  let fileToUpload = <File>files[0];
  const formData = new FormData();
  formData.append('file', fileToUpload, fileToUpload.name);
  
  this.http.post(environment.ServerUrl+'/api/Car/Image', formData, {reportProgress: true, observe: 'events'})
    .subscribe({
      next: (event) => {
      if (event.type === HttpEventType.UploadProgress)
        this.progress = Math.round(100 * event.loaded / event.total!);
      else if (event.type === HttpEventType.Response) {
        this.message = 'Upload success.';
        this.onUploadFinished.emit(event.body);
      }
    },
    error: (err: HttpErrorResponse) => console.log(err)
  });
}
}