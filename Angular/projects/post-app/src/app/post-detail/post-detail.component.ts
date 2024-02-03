import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostService } from '../services/post.service';
import { Subject, takeUntil } from 'rxjs';
import { PostDto } from '../entites/post.dto';
import { format } from 'date-fns';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrl: './post-detail.component.scss'
})
export class PostDetailComponent implements OnInit {

  private ngUnsubscribe = new Subject<void>();

  postDto!: PostDto;
  idPost: string = '';
  
  constructor(private postService: PostService) { }

  ngOnInit() {
    this.idPost = JSON.parse(localStorage.getItem('idPost')!)
    this.getPostById(this.idPost);
  }

  getPostById(id: string){
    this.postService.getPostById(id)
      .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
              next: (response: PostDto) => {
                this.postDto = response;
                console.log("POST BY IDaa", this.postDto);
              },
              error: () => {},
    })
  }

  formatDate(date: Date): string {
    return format(date, 'yyyy-MM-dd HH:mm:ss');
  }
}
