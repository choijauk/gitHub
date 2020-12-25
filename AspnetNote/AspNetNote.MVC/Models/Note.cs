using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetNote.MVC.Models
{
    public class Note
    {
        /// <summary>
        /// 게시물 번호
        /// </summary>
        [Key]
        public int NoteNo { get; set; }

        /// <summary>
        /// 게시물 제목
        /// </summary>
        [Required(ErrorMessage = "제목을 입력하세요.")]  // Not Null 설정
        public string NoteTitle { get; set; }

        /// <summary>
        /// 게시물 내용
        /// </summary>
        [Required(ErrorMessage = "내용을 입력하세요.")]  // Not Null 설정
        public string NoteContents { get; set; }

        /// <summary>
        /// 작성자 번호
        /// </summary>
        [Required]
        public int UserNo { get; set; }

        [ForeignKey("UserNo")]
        public virtual User User { get; set; }  //virtual - Razy Loading?? 이란걸 하기 위해 - 비동기 적으로 중요 필드 로드 후에 로딩
    }
}
