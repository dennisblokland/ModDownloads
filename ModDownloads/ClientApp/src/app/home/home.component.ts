import { Component,} from '@angular/core';
import { DownloadService } from '../download.service'
import { ModService } from '../mod.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  public DownloadData: Array<any>;
  public totalDownlaods: number;

  constructor(private downloadService: DownloadService, private modService: ModService, private router: Router) {
    modService.get().subscribe((data: Array<any>) => {
      let DownloadData2 = [];
      for (let entry of data) {
        downloadService.getById(entry).subscribe((data: Array<any>) => {
          let set = { name: entry.name, series: [] };
          for (let entry of data) {
            set.series.push({
              name: new Date(entry.timestamp),
              value: entry.downloads
            });
          }

          DownloadData2.push(set);
          this.DownloadData = [...DownloadData2];

        });
      }
      
      
    });
    downloadService.getTotal().subscribe((data: number) => {
      this.totalDownlaods = data;
    });


  }

  view: any[] = [1200, 800];

  // options for the chart
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Downloads';
  showYAxisLabel = true;
  yAxisLabel = 'Time';
  timeline = true;

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  // line, area
  autoScale = true;
  onSelect(event) {
    this.router.navigate(['/detail', event]);
  }
}
