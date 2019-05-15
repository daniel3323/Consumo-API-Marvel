using ConsumindoApiMarvel.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsumindoApiMarvel.View
{
    public partial class frmPesquisarHeroi : Form
    {
        private ControllerHeroi controllerHeroi = new ControllerHeroi();

        public frmPesquisarHeroi()
        {
            InitializeComponent();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            lblID.Text = controllerHeroi.GetHeroiAPI(txtNomePesquisa.Text).Nome;
            lblDescricao.Text = controllerHeroi.GetHeroiAPI(txtNomePesquisa.Text).Historia;
            picFotoHeroi.Load(controllerHeroi.GetHeroiAPI(txtNomePesquisa.Text).LinkFoto);
        }
    }
}
