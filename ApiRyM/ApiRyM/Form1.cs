using ApiRyM.controllers;
using ApiRyM.models;

namespace ApiRyM
{
    public partial class Form1 : Form
    {
        private CharactersController _CharactersController;
        private Characters _characters;

        public Form1()
        {
            InitializeComponent();
            _characters = new Characters();
            _CharactersController = new CharactersController();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void GetCharacters()
        {
            _characters = await _CharactersController.GetAllCharacters();

            if (_characters != null)
            {
                foreach (var character in _characters?.results!)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dataGridView1);
                    row.Cells[0].Value = character.name;
                    row.Cells[1].Value = character.gender;
                    row.Cells[2].Value = character.species;
                    row.Cells[3].Value = character.origin.name;

                    dataGridView1.Rows.Add(row);
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetCharacters();
        }
    }
}
