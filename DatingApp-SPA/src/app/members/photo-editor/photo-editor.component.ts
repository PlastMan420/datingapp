import { Component, OnInit, Input } from '@angular/core';
import { Photo } from 'src/app/_models/photo';
import { FileUploader } from 'ng2-file-upload';
import { environment } from 'src/environments/environment';
import { AuthService } from 'src/app/services/auth.service';
import { reduce } from 'rxjs/operators';
import { UserService } from 'src/app/services/user.service';
import { AlertifyService } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-photo-editor',
  templateUrl: './photo-editor.component.html',
  styleUrls: ['./photo-editor.component.css']
})
export class PhotoEditorComponent implements OnInit {
  // when bringing data from a parent component. use @Input decorator
  @Input() photos: Photo[];
  baseUrl = environment.apiUrl;
  uploader: FileUploader;
  hasBaseDropZoneOver: boolean;
  response: string;
  constructor(private authService: AuthService, private userService: UserService, private alertify: AlertifyService) {
    this.uploader = new FileUploader({
      // url: URL,
      disableMultipart: true, // 'DisableMultipart' must be 'true' for formatDataFunction to be called.
      formatDataFunctionIsAsync: true,
      formatDataFunction: async (item) => {
        return new Promise( (resolve, reject) => {
          resolve({
            name: item._file.name,
            length: item._file.size,
            contentType: item._file.type,
            date: new Date()
          });
        });
      }
    });

    this.hasBaseDropZoneOver = false;

    this.response = '';

    this.uploader.response.subscribe( res => this.response = res );
  } // constructor

  ngOnInit() {
    this.initializeUploader();
  }
  public fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl + 'users/' + this.authService.decodedToken.nameid + '/photos',
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image'],
      removeAfterUpload: true, // after photo is uploaded, remove from upload queue.
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024, // 10MB
    });
    this.uploader.onAfterAddingFile = (file) => {file.withCredentials = false; }; // fixes the CORS error that appears in console.
    this.uploader.onSuccessItem = (item, response, status, headers) => {
      if (response) {
        const res: Photo = JSON.parse(response);
        const photo = {
          id: res.id,
          url: res.url,
          dateAdded: res.dateAdded,
          description: res.description,
          isMain: res.isMain
        };
        this.photos.push(photo);
      }
    };
  }
  setMainPhoto(photo: Photo) {
    this.userService.setMainPhoto(this.authService.decodedToken.nameid, photo.id).subscribe(() => {
      console.log('Successfully');
    }, error => {
      this.alertify.error(error);
    });
  }
}
