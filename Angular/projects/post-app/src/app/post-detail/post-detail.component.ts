import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { PostService } from '../services/post.service';
import { Subject, takeUntil } from 'rxjs';
import { PostDto } from '../entites/post.dto';
import { format, formatDistanceToNow } from 'date-fns';
import { CreateCommentDto, TreeComment } from '../entites/comment.post.dto';
import { User } from 'oidc-client-ts';

@Component({
  selector: 'app-post-detail',
  templateUrl: './post-detail.component.html',
  styleUrl: './post-detail.component.scss'
})
export class PostDetailComponent implements OnInit {

  private ngUnsubscribe = new Subject<void>();

  postDto!: PostDto;
  commentPostDto!: any;
  idPost: string = '';
  createCommentDto: CreateCommentDto = {
    postId: "",
    ancestorId: null,
    author: "",
    content: ""
  };
  @ViewChild('textInput', { static: false }) textInput!: ElementRef;
  
  constructor(private postService: PostService) { }

  ngOnInit() {
    this.idPost = JSON.parse(localStorage.getItem('idPost')!)
    this.getPostById(this.idPost);
    this.getAllComment(this.idPost);
  }

  formatDate(date: Date): string {
    return format(date, 'yyyy-MM-dd HH:mm:ss');
  }

  formatHours(date: Date): any {
    return  formatDistanceToNow(date, { addSuffix: true });
  }

  isParent(item: any): boolean {
    return item.ancestorId == null;
  }

  focusOnInput(id: number) {
    this.createCommentDto.ancestorId = id;
    this.textInput.nativeElement.value = '';
    this.textInput.nativeElement.focus();
  }

  onEnterKeyPress(content: string) {
    const user: User = JSON.parse(localStorage.getItem('user')!);
    this.createCommentDto.postId = this.idPost;
    this.createCommentDto.author = user.profile.sub;
    this.createCommentDto.content = content;
    this.createComment(this.createCommentDto);
    this.textInput.nativeElement.value = '';
  }

  // API Call
  getPostById(id: string){
    this.postService.getPostById(id)
      .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
              next: (response: PostDto) => {
                this.postDto = response;
              },
              error: () => {},
    })
  }

  getAllComment(id: string){
    this.postService.getAllComment(id)
      .pipe(takeUntil(this.ngUnsubscribe))
            .subscribe({
              next: (response: TreeComment) => {
                this.commentPostDto = response;
              },
              error: () => {},
    })
  }

  createComment(createCommentDto: CreateCommentDto){
    this.postService
      .createComment(createCommentDto)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.getAllComment(this.idPost);
        },
        error: () => {}
      })
  }

  removeComment(id: number){
    this.postService
      .deleteComment(id)
      .pipe(takeUntil(this.ngUnsubscribe))
      .subscribe({
        next: () => {
          this.getAllComment(this.idPost);
        },
        error: () => {}
      })
  }
  
}