import { Component, OnInit } from '@angular/core';
import {DownloadService} from "../download.service";
import {ModService} from "../mod.service";
import {Router} from "@angular/router";

@Component({
  selector: 'app-mod',
  templateUrl: './mod.component.html',
  styleUrls: ['./mod.component.css']
})

export class ModComponent implements OnInit {

  public mods: object;
  constructor(private downloadService: DownloadService, private modService: ModService, private router: Router) {
  }

    ngOnInit() {
    this.modService.get().subscribe((data: Array<any>) => {
      this.mods = data;
      for (let entry of data) {
          this.downloadService.getTotalById(entry).subscribe((data: number) => {
            entry.downloads = data
          });

      }
    });
  }

}
