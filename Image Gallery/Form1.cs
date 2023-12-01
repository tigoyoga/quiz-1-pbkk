using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace Image_Gallery
{
    public partial class Form1 : Form
    {
        private int SelectedImageIndex = 0;
        // count image
        private int ImageIndex = 1;

        private List<Image> LoadedImages { get; set; }
        ImageList images = new ImageList();
        public Form1()
        {
            InitializeComponent();
            LoadedImages = new List<Image>();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void uploadFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.jpg;*.png)|*.jpg;*.png";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Load image
                Image image = Image.FromFile(openFileDialog.FileName);

                // Add image to list
                LoadedImages.Add(image);


                // Add image to image list

                images.ImageSize = new Size(120, 60);
                images.Images.Add(image);



                // Set image list to list view
                if (images.Images.Count > 0)
                {
                    imageList.Visible = true;
                    selectedImage.Visible = true;
                    menuStrip1.Visible = true;
                    NextImgBtn.Visible = true;
                    PrevImgBtn.Visible = true;
                    menuStrip1.Visible = true;
                }
                else
                {
                    imageList.Visible = true;
                    selectedImage.Visible = true;
                    menuStrip1.Visible = true;
                    NextImgBtn.Visible = true;
                    PrevImgBtn.Visible = true;
                    menuStrip1.Visible = true;
                }


                imageList.LargeImageList = images;
                imageList.Items.Add(new ListViewItem($"Image {ImageIndex}", ImageIndex - 1));

                ImageIndex++;

            }
        }

        private void SelectTheClickedItem(ListView list, int index)
        {
            for (int item = 0; item < list.Items.Count; item++)
            {
                if (item == index)
                {
                    list.Items[item].Selected = true;
                }
                else
                {
                    list.Items[item].Selected = false;
                }
            }
        }

        private void imageList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (imageList.SelectedIndices.Count > 0)
            {
                var selectedIndex = imageList.SelectedIndices[0];

                Image selectedImg = LoadedImages[selectedIndex];
                selectedImage.Image = selectedImg;
                SelectedImageIndex = selectedIndex;

            }



        }

        private void PrevImgBtn_Click(object sender, EventArgs e)
        {
            if (SelectedImageIndex > 0)
            {
                SelectedImageIndex -= 1;
                Image selectedImg = LoadedImages[SelectedImageIndex];
                selectedImage.Image = selectedImg;
                SelectTheClickedItem(imageList, SelectedImageIndex);
            }
        }

        private void NextImgBtn_Click(object sender, EventArgs e)
        {
            if (SelectedImageIndex < (LoadedImages.Count - 1))
            {
                SelectedImageIndex += 1;
                Image selectedImg = LoadedImages[SelectedImageIndex];
                selectedImage.Image = selectedImg;
                SelectTheClickedItem(imageList, SelectedImageIndex);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (imageList.SelectedIndices.Count > 0)
            {
                int selectedIndex = imageList.SelectedIndices[0];

                // Hapus gambar dari ImageList
                
               

                // Hapus item dari ListView
                imageList.Items.RemoveAt(selectedIndex);

                // Hapus gambar dari LoadedImages
                LoadedImages.RemoveAt(selectedIndex);

                // Reset indeks gambar yang terpilih
                SelectedImageIndex = 0;

                if (LoadedImages.Count > 0)
                {
                    selectedImage.Image = LoadedImages[0];
                }
                else
                {
                    // Jika tidak ada gambar tersisa, sembunyikan komponen yang menampilkan gambar
                    selectedImage.Image = null;
                }
            }
        }
    }
}
