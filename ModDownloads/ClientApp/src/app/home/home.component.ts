import { Component,} from '@angular/core';
import { DownloadService } from '../download.service'
import { ModService } from '../mod.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent {

  private DownloadData: Array<any>;

  constructor(private downloadService: DownloadService, private modService: ModService) {
    modService.get().subscribe((data: Array<any>) => {
      let DownloadData2 = [];
      for (let entry of data) {
        downloadService.getById(entry).subscribe((data: Array<any>) => {
          let set = { name: entry.name, series: [] };
          set.name = entry.name;
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

  }

  view: any[] = [1200, 800];

  // options for the chart
  showXAxis = true;
  showYAxis = true;
  gradient = false;
  showLegend = true;
  showXAxisLabel = true;
  xAxisLabel = 'Number';
  showYAxisLabel = true;
  yAxisLabel = 'Value';
  timeline = true;

  colorScheme = {
    domain: ['#5AA454', '#A10A28', '#C7B42C', '#AAAAAA']
  };

  // line, area
  autoScale = true;
  onSelect(event) {
    console.log(event);
  }
}
